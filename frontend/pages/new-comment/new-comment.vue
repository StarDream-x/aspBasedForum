<template>
	<view class="new-comment">
		<textarea name="comment" id="comment" cols="30" rows="10" placeholder="评论内容..."></textarea>
		<button class="button-submit" type="primary">发布</button>
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
	.new-comment {
		width: 750rpx;
	}
	
	#comment {
		border-bottom: 1px solid lightgrey;
		box-sizing: border-box;
		padding: 40rpx;
		width: 750rpx;
		height: 1000rpx;
	}
	
	.button-submit {
		margin-top: 60rpx;
		width: 500rpx;
	}
</style>
