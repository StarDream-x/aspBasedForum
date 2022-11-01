<template>
	<view class="title">
		<text class="title-text">Welcome!</text>
	</view>
	<view class="panel">
		<view class="field">
			<label for="account">账户：</label>
			<input type="text" id="account" v-model="username" focus placeholder="请输入账户名">
		</view>
		<view class="field">
			<label for="password">密码：</label>
			<input type="safe-password" id="password" v-model="password" :password="showPassword" focus placeholder="请输入密码">
		</view>
		<button class="login-button" type="primary" @click="login">登录</button>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				username: '',
				password:'',
				showPassword:true
			}
		},
		methods: {
			login() {
				myRequest({
					url: 'login',
					method: 'POST',
					data: {
						username: this.username,
						password: this.password
					}
				}).then((res) => {
					if (res.statusCode == 200) {
						try {
							uni.setStorageSync('token', res.data.token);
							uni.setStorageSync('userId',this.data.username);
						} catch (e) {
							uni.showToast({
								icon: "none",
								title: "存储token失败"
							})
						}
					}
					let token = uni.getStorageSync('token');
					if (token) {
						// TODO:进入首页
						uni.reLaunch({
							url:"/pages/contents/contents",
						})
					
					} else {
						uni.showToast({
							icon: "none",
							title: "用户名或密码错误！"
						})
					}
				})
			}
		}
	}
</script>

<style>
	page {
		height: 100%;
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
	}

	.panel {
		border: 1rpx solid lightgrey;
		padding: 120rpx 60rpx;
		border-radius: 8px;
		margin-bottom: 80rpx;
	}

	.login-button {
		margin-top: 120rpx;
		width: 300rpx;
	}

	.title {
		margin-left: auto;
		margin-right: auto;
		margin-bottom: 200rpx;
	}

	.title-text {
		font-size: 80rpx;
	}

	label {
		font-size: 40rpx;
	}

	.field {
		margin: 40rpx;

		display: flex;
		align-items: baseline;
	}

	input {
		width: 350rpx;
		border: 1rpx solid lightgrey;
		border-radius: 4px;
		display: inline-block;
		padding: 10rpx;
	}
</style>
