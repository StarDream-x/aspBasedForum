<template>
	<view class="title">
		<text class="title-text">Welcome!</text>
	</view>
	<view class="panel">
		<view class="field">
			<label for="account">账户：</label>
			<input type="text" id="account">
		</view>
		<view class="field">
			<label for="password">密码：</label>
			<input type="safe-password" id="password">
		</view>
		<view class="field">
			<label for="confirm-password">确认密码：</label>
			<input type="safe-password" id="confirm-password">
		</view>
		<button class="login-button" type="primary">注册</button>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				
			}
		},
		methods: {
			
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
