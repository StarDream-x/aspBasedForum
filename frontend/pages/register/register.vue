<template>
	<view class="title">
		<text class="title-text">Welcome!</text>
	</view>
	<view class="panel">
		<view class="field">
			<label for="account">账户：</label>
			<input type="text" id="account" v-model="username" @blur="checkAccount">
		</view>
		<view class="field">
			<label for="password">密码：</label>
			<input type="safe-password" id="password" v-model="password1">
		</view>
		<view class="field">
			<label for="confirm-password">确认密码：</label>
			<input type="safe-password" id="confirm-password" v-model="password2">
		</view>
		<button class="login-button" type="primary" @click="register">注册</button>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				username: '',
				password1: '',
				password2: '',
				unique:null
			}
		},
		methods: {
			register() {
				if(this.unique==true){
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
									uni.setStorageSync('token', res.data.token);
								} catch (e) {
									uni.showToast({
										icon: "none",
										title: "存储token失败"
									})
								}
								//TODO 进入首页
								uni.reLaunch({
									url:"/pages/contents/contents",
								})
							}
						})
					}
				}
				else{
					uni.showToast({
						icon: "none",
						title: "账户重复，请重新填写"
					})
				}
			},
			checkAccount(e){
				// console.log("触发")
				myRequest({
					url:"register/checkId",
					method:"GET",
					data:{
						id:this.username
					}
				}).then((res)=>{
					if (res.statusCode == 200){
						this.unique = res.data.valid
					}				
				})
				// console.log(this.unique)
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
