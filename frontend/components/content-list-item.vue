<template>
	<view class="list-item">
		<image class="cover" :src="this.imageSrc || '/static/logo.png'" mode="aspectFill"></image>
		<view class="item-info">
			<text class="title">{{title || '标题'}}</text>
			<view class="item-info__bottom">
				<text class="author">{{author || '作者'}}</text>
				<text class="view-count">{{viewCount || '阅读量'}}</text>
			</view>
		</view>
	</view>
</template>

<script>
	export default {
		name:"content-list-item",
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
	.list-item {
		width: 100%;
		height: 220rpx;
		border-bottom: 1px solid lightgrey;
		
		display: flex;
		align-items: center;
		justify-content: space-around;
	}
	
	.cover {
		width: 160rpx;
		height: 160rpx;
	}
	
	.item-info {
		width: 500rpx;
		height: 160rpx;
		
		display: flex;
		flex-direction: column;
		justify-content: space-between;
	}
	
	.title {
		font-size: 40rpx;
		font-weight: 500;
		
		display: -webkit-box;
		-webkit-box-orient: vertical;
		white-space: nowrap;
		-webkit-line-clamp: 2;
		overflow: hidden;
		text-overflow: ellipsis;
	}
	
	.item-info__bottom {
		display: flex;
		justify-content: space-between;
		align-items: baseline;
	}
	
	.author {
		width: 300rpx;
		overflow: hidden;
		text-overflow: ellipsis;
		white-space: nowrap;
		color: #999;
	}
	
	.view-count {
		width: 150rpx;
		text-align: end;
		color: #999;
		overflow: hidden;
		text-overflow: ellipsis;
		white-space: nowrap;
	}
</style>