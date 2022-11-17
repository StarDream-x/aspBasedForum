<template>
	<uni-file-picker title="选择头像" class="avatar-picker" v-model="this.avatarValue" disable-preview :del-icon="false" return-type="object" @select="select" @delete="deletePic">选择头像</uni-file-picker>
	<button type="primary" @click="update">上传</button>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				avatarValue: {
					name: '/img/BodyPart_5bc63f3e-180c-4193-8a64-6837e9ab94f0',
					extName: '/img/BodyPart_5bc63f3e-180c-4193-8a64-6837e9ab94f0',
					url: '/img/BodyPart_5bc63f3e-180c-4193-8a64-6837e9ab94f0'
				},
				filePath: [],
				media: []
			}
		},
		methods: {
			select(e) {
				this.uploadImg(e.tempFilePaths[0]);
			},
			uploadImg(path) {
				this.filePath.push(path);
				uni.uploadFile({
					url: 'http://localhost/me/avatarUrl',
					filePath: path,
					name: 'file',
					success: res => {
						const resp = JSON.parse(res.data)
						this.media.push(resp.fileUrl);
					}
				});
			},
			deletePic(e) {
				const num = this.filePath.findIndex(v => v.url === e.tempFilePath);
				this.media.splice(num, 1);
				this.filePath.splice(num, 1);
			},
			update(){
				uni.navigateBack()
			}
		}
	}
</script>

<style>
	.info-list {
		display: flex;
		flex-direction: column;
	}
	
	.info-item {
		display: flex;
		align-items: center;
		justify-content: space-between;
		border-bottom: 1px solid lightgrey;
	}
	
	.info-item-avatar {
		display: flex;
		flex-direction: column;
		
	}
	
	.avatar-picker {
	}
	
	.info-title {
		font-size: 36rpx;
		color: #999;
	}
	
	input {
		padding: 16rpx;
	}
	
	button {
		margin: 40rpx 150rpx;
	}
</style>
