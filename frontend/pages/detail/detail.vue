<template>
	<view class="main">
		<view class="author-info">
			<image class="avatar" src="/static/logo.png" mode="aspectFill"></image>
			<text class="author-name">作者名</text>
		</view>
		<swiper class="pictures">
			<swiper-item>
				<image src="../../static/logo.png" mode="aspectFit"></image>
			</swiper-item>
			<swiper-item>
				<image src="../../static/logo.png" mode="aspectFit"></image>
			</swiper-item>
			<swiper-item>
				<image src="../../static/logo.png" mode="aspectFit"></image>
			</swiper-item>
		</swiper>
		<view class="text">中文文字Lorem ipsum dolor sit amet consectetur adipisicing elit. Totam nobis hic eligendi ipsum accusantium numquam dolor ad, quam porro ut id. Facere ipsa similique accusantium itaque perspiciatis incidunt quas ab.</view>
		<view class="comments"></view>
	</view>
	<view class="bottom-panel">
		<view class="bottom-item">
			<image v-if="this.liked" class="bottom-item__icon" src="~@/static/icon/like_primary.svg" mode="aspectFill"></image>
			<image v-else class="bottom-item__icon" src="~@/static/icon/like.svg" mode="aspectFill"></image>
			<text class="bottom-item__text">114</text>
		</view>
		<view class="bottom-item">
			<image v-if="this.favorited" class="bottom-item__icon" src="~@/static/icon/favorite_primary.svg" mode="aspectFill"></image>
			<image v-else class="bottom-item__icon" src="~@/static/icon/favorite.svg" mode="aspectFill"></image>
			<text class="bottom-item__text">114</text>
		</view>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				liked: true,
				favorited: false
			}
		},
		methods: {
			
		}
	}
</script>

<style>
	page {
		display: flex;
		flex-direction: column;
		align-items: center;
	}
	
	.main {
		width: 750rpx;
		box-sizing: border-box;
		padding-bottom: 100rpx;
	}
	
	.author-info {
		width: 750rpx;
		height: 100rpx;
		border-bottom: 1px solid lightgrey;
		padding-left: 60rpx;
		box-sizing: border-box;
		display: flex;
		align-items: center;
	}
	
	.avatar {
		width: 60rpx;
		height: 60rpx;
		border-radius: 50%;
	}
	
	.author-name {
		font-weight: bold;
		font-size: 36rpx;
		margin-left: 40rpx;
	}
	
	.pictures {
		width: 750rpx;
		height: 1000rpx;
	}
	
	.pictures image {
		background-color: black;
		width: 100%;
		height: 100%;
		box-sizing: border-box;
	}
	
	.text {
		padding: 40rpx;
		font-size: 36rpx;
		box-sizing: border-box;
	}
	
	.bottom-panel {
		position: fixed;
		bottom: 0;
		
		width: 750rpx;
		height: 100rpx;
		box-sizing: border-box;
		padding-right: 40rpx;
		z-index: 5;
		background-color: white;
		border-top: 1px solid lightgrey;
		
		display: flex;
		justify-content: flex-end;
	}
	
	.bottom-item {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		margin-left: 40rpx;
	}
	
	.bottom-item__icon {
		width: 50rpx;
		height: 50rpx;
	}
	
	.bottom-item__text {
		color: #999;
		font-size: 20rpx;
	}
</style>
