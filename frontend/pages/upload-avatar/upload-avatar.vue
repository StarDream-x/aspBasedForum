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
					name: '',
					extName: '',
					url: ''
				},
				url: ''
			}
		},
		methods: {
			select(e) {
				this.uploadImg(e.tempFilePaths[0]);
			},
			uploadImg(path) {
				uni.uploadFile({
					url: 'http://localhost/media',
					filePath: path,
					name: 'file',
					success: res => {
						const resp = JSON.parse(res.data)
						this.url = resp.fileUrl;
					}
				});
			},
			update(){
				myRequest({
					url: 'me/avatarUrl',
					method: 'PATCH',
					data: {
						avatarUrl: this.url
					}
				}).then(() => {
					uni.showToast({
						title: '修改成功',
						icon: 'success',
						success() {
							uni.reLaunch({
								url:"/pages/user/user"
							})
						}
					})
				})
			}
		},
		onLoad() {
			const preAvatar = getApp().globalData.preAvatar
			this.avatarValue = {
				name: preAvatar,
				extName: preAvatar,
				url: preAvatar
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
