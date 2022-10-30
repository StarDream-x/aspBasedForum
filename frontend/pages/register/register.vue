<template>
	<view class="title">
		<text class="title-text">Welcome!</text>
	</view>
	<view class="panel">
		<view class="field">
			<label for="account">账户：</label>
			<input type="text" id="account" v-model:value="username">
		</view>
		<view class="field">
			<label for="password">密码：</label>
			<input type="safe-password" id="password" v-model:value="password1">
		</view>
		<view class="field">
			<label for="confirm-password">确认密码：</label>
			<input type="safe-password" id="confirm-password" v-model:value="password2">
		</view>
		<button class="login-button" type="primary" @click="register">注册</button>
	</view>
</template>

<script>
	import {
		myRequest
	} from '~@/util/api.js'
	export default {
		data() {
			return {
				username: '',
				password1: '',
				password2: ''
			}
		},
		methods: {
			register() {
				if(this.password1 != this.password2){
					uni.showToast({
						icon: "none",
						title: "两次密码不一致，请重试"
					})
					return;
				}
				if (this.password1 == this.password2) {
					myRequest({
						url:"register",
						method:"POST",
						data:{
							username:this.username,
							password:this.password1
						}
					}).then((res)=>{
						if (res.statusCode == 200) {
							try {
								uni.setStorageSync('token', token);
							} catch (e) {
								uni.showToast({
									icon: "none",
									title: "存储token失败"
								})
							}
							//TODO 进入首页
						}
					})
				}
			}

		}
	}
</script>

<style>
	page {
		height: 100vh;
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
	}

	.panel {
		display: flex;
		flex-direction: column;
		align-items: flex-end;
		justify-content: center;

		border: 1rpx solid lightgrey;
		padding: 120rpx 0rpx;
		border-radius: 8px;
		margin-bottom: 80rpx;
	}

	.login-button {
		margin-top: 120rpx;
		width: 300rpx;
	}

	.title {
		margin-bottom: 200rpx;
	}

	.title-text {
		font-size: 80rpx;
	}

	label {
		font-size: 40rpx;
	}

	.field {
		margin: 20rpx;

		display: flex;
		align-items: baseline;
	}

	input {
		border: 1rpx solid lightgrey;
		border-radius: 4px;
		display: inline-block;
		padding: 10rpx;
	}
</style>
