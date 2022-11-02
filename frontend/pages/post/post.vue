<template>
	<view>
		<uni-file-picker v-model="imageValue" title="选择图片" :limit="9" file-mediatype="image" mode="grid"
			@select="select" @delete="deletepic"></uni-file-picker>
		<textarea v-module="body" name="text-content" id="text-content" cols="30" rows="20" placeholder="写下你想说的话吧!"></textarea>
		<button class="post-button" type="primary" @click="postContents">发布</button>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				imageValue: [],
				FilePath:[],
				media:[],
				title:"",
				body:"",
			}
		},
		methods: {
			select(e) {
				console.log("选择了图片")
				console.log(e)
				await this.uploadImg(e.tempFilePaths);
			},
			deletepic(e){
				console.log("删除了图片")
				console.log(e)
				const num = this.FilePath.findIndex(v => v.url === e.tempFilePath);
				this.media.splice(num, 1);
				this.FilePath.splice(num, 1);
			},
			uploadImg(tempFilePaths) {
				const tempFilePaths = tempFilePaths;
				this.FilePath = [...this.FilePath,...tempFilePaths]
				uni.uploadFile({
					url: 'https://localhost:8080/content/media', 
					filePath: tempFilePaths,
					name: 'file',
					// formData: {
					// 	'file': 'test'
					// },
					success: (uploadFileRes) => {
						//console.log(uploadFileRes.data);
						this.media = [...this.media,...uploadFileRes.data.fileUrl]
					}
				});
			},
			postContents(){
				myRequest({
					url: 'content',
					method: 'POST',
					data: {
						title:this.title,
						body:this.body,
						media:this.media
					}
				}).then((res) => {
					if(res.statusCode == 200){
						uni.showToast({
							title: '发布成功！',
							icon:"success"
						});
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
