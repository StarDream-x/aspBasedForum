<template>
	<view>
		<uni-file-picker title="选择图片" :limit="9" file-mediatype="image" mode="grid" @select="select" @delete="deletePic"></uni-file-picker>
		<input type="text" class="title" placeholder="标题" v-model="this.title" />
		<textarea name="text-content" id="text-content" cols="30" rows="20" placeholder="写下你想说的话吧!" v-model="this.body"></textarea>
		<button class="post-button" type="primary" @click="postContent">发布</button>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				filePath: [],
				media: [],
				title:null,
				body:null
			}
		},
		methods: {
			select(e) {
				this.uploadImg(e.tempFilePaths[0]);
			},
			uploadImg(path) {
				this.filePath.push(path);
				uni.uploadFile({
					url: 'http://localhost:3000/content/media',
					filePath: path,
					name: 'file',
					success: res => {
						this.media.push(res.data.fileUrl);
					}
				});
			},
			deletePic(e) {
				const num = this.filePath.findIndex(v => v.url === e.tempFilePath);
				this.media.splice(num, 1);
				this.filePath.splice(num, 1);
			},
			postContent(e) {
				myRequest({
					url: 'content',
					method: 'POST',
					data: {
						title: this.title,
						body: this.body,
						media: this.media
					}
				}).then((res) => {
					if(res.statusCode == 200){
						uni.showToast({
							title: '发布成功！',
							icon:"success"
						});
						uni.reLaunch({
							url:"/pages/contents/contents"
						})
					}
				})
			}
		}
	}
</script>

<style>
	.uni-file-picker {
		width: 750rpx;
		margin-bottom: 50rpx;
	}
	
	.title {
		width: 750rpx;
		padding: 50rpx;
		border-top: 1px solid lightgrey;
		font-size: 36rpx;
		font-weight: bold;
	}
	
	#text-content {
		width: 750rpx;
		padding: 50rpx;
		border-top: 1px solid lightgray;
		border-bottom: 1px solid lightgray;
		margin-bottom: 50rpx;
		
		box-sizing: border-box;
	}
	
	.post-button {
		width: 500rpx;
		margin: auto;
	}
</style>
