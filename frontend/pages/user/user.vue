<template>
	<view class="user-info">		
		<image class="avatar" src="../../static/logo.png" mode="aspectFill"></image>
		<text class="username">昵称</text>
		<view class="data-row">
			<view class="data-item data-item-following">
				<text class="data-item__data">1680</text>
				<text class="data-item__name">关注</text>
			</view>
			<view class="data-item data-item-follower">
				<text class="data-item__data">1111</text>
				<text class="data-item__name">粉丝</text>
			</view>
			<view class="data-item data-item-favorite">
				<text class="data-item__data">1111</text>
				<text class="data-item__name">收藏</text>
			</view>
			<view class="data-item data-item-like">
				<text class="data-item__data">1111</text>
				<text class="data-item__name">点赞</text>
			</view>
		</view>
	</view>
	<view class="post-list">
		<view class="post-list__head">发布内容</view>
		<view class="post-list__body">
			<ContentListItem title="一个一个一个一个一个标题" author="一个一个一个一个一个一个一个一个作者" viewCount="一个阅读量" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
			<ContentListItem title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
		</view>
	</view>
</template>

<script>	
	import ContentListItem from '~@/components/content-list-item.vue'
	export default {
		data() {
			return {
				
			}
		},
		components: {
			ContentListItem
		},
		methods: {
			
		}
	}
</script>

<style>
	.user-info {
		display: flex;
		flex-direction: column;
		align-items: center;
		margin-bottom: 40rpx;
	}

	.avatar {
		width: 250rpx;
		height: 250rpx;
		border-radius: 50%;
		margin: 40rpx;
	}
	
	.username {
		font-weight: bold;
		font-size: 48rpx;
		margin-bottom: 40rpx;
	}
	
	.data-row {
		display: flex;
		width: 550rpx;
		justify-content: space-between;
	}
	
	.data-item {
		display: flex;
		flex-direction: column;
		align-items: center;
	}
	
	.data-item__data {
		font-weight: bold;
		font-size: 48rpx;
		margin-bottom: 16rpx;
	}
		
	.data-item__name {
		color: #999;
		font-size: 36rpx;
	}
	
	.post-list__head {
		border-top: 1px solid lightgrey;
		border-bottom: 1px solid lightgrey;
		padding: 16rpx;
		
		font-size: 36rpx;
		color: #999;
	}
</style>
