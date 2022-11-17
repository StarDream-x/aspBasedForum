<template>
	<uni-file-picker title="选择头像" class="avatar-picker" v-model="this.avatarValue" disable-preview :del-icon="false" return-type="object">选择头像</uni-file-picker>
	<button type="primary">上传</button>
</template>

<script>
	export default {
		data() {
			return {
				avatarValue: {
					name: '/img/BodyPart_5bc63f3e-180c-4193-8a64-6837e9ab94f0',
					extName: '/img/BodyPart_5bc63f3e-180c-4193-8a64-6837e9ab94f0',
					url: '/img/BodyPart_5bc63f3e-180c-4193-8a64-6837e9ab94f0'
				},
				username: ''
			}
		},
		methods: {
			
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
