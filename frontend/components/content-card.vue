<template>
	<view class="container">
		<view class="card">
			<image src="../static/logo.png" mode="aspectFill"></image>
			<text class="view-count">阅读量</text>
			<text class="author">作者</text>
			<text class="title">标题</text>
		</view>
	</view>
</template>

<script>
	export default {
		name:"content-card",
		data() {
			return {
				
			};
		}
	}
</script>

<style>
	.container {
		width: 375rpx;
		height: 375rpx;
	}
	
	
</style>