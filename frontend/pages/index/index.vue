<template>
	<view class="title">
		<text class="title-text">Welcome!</text>
	</view>
	<view class="buttons">
		<navigator url="../login/login" hover-class="navigator-hover">
			<button type="primary">登录</button>
		</navigator>
		<navigator url="../register/register" hover-class="navigator-hover">
			<button>注册</button>
		</navigator>
	</view>
</template>

<script>
	export default {
		data() {
			
		},
		onLoad() {

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
		justify-content: space-evenly;
	}
	
	.title-text {
		font-size: 80rpx;
	}
	
	button {
		margin: 80rpx;
		width: 300rpx;
	}
</style>
