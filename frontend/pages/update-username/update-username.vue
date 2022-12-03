<template>
	<input type="text" class="username" v-model="this.username">
	<button type="primary" class="submit-button" @click="update">完成修改</button>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				username: ''
			}
		},
		methods: {
			update(){
				myRequest({
					url: "me/username",
					method: 'PATCH',
					data:{
						username:this.username
					}
				}).then((res) => {
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
			this.username = getApp().globalData.preUsername
		}
	}
</script>

<style>
	.username {
		padding : 40rpx;
		border-bottom: 1px solid lightgrey;
	}
	
	.submit-button {
		margin: 40rpx 150rpx;
	}
</style>
