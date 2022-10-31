<template>
	<view class="card">
		<image class="cover" :src="this.imageSrc || '/static/logo.png'" mode="aspectFill"></image>
		<view class="info">
			<view class="info__content">
					<text class="title">{{title || '标题'}}</text>
				<view class="info__content__bottom">					
					<text class="author">{{author || '作者'}}</text>
				<text class="view-count">{{viewCount || '阅读量'}}</text>
				</view>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		name:"content-card",
		props: [
			'viewCount',
			'author',
			'title',
			'imageSrc'
		],
		data() {
			return {
				
			};
		}
	}
</script>

<style>
	.card {
		width: 360rpx;
		height: 640rpx;
		position: relative;
		z-index: 10;
	}
	
	.info {
		width: 100%;
		height: 20%;
		bottom: 0;
		position: absolute;
		z-index: 5;
	}
	
	.info__content {
		width: 100%;
		height: 100%;
		position: relative;
		padding: 16rpx;
		box-sizing: border-box;
	}
	
	.info__content__bottom {
		display: flex;
		align-items: baseline;
		justify-content: space-between;
	}
	
	.title {
		display: block;
		font-weight: bold;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
	
	.author {
		font-size: 4rpx;
		display: inline-block;
		width: 60%;
		overflow: hidden;
		white-space: nowrap;
		text-overflow: ellipsis;
	}
	
	.view-count {
		font-size: 4rpx;
		display: inline-block;
	}
	
	.cover {
		width: 100%;
		height: 80%;
		border-radius: 4px;
	}
</style>