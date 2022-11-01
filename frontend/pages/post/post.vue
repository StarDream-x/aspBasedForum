<template>
	<view>
		<uni-file-picker title="选择图片" :limit="9" file-mediatype="image" mode="grid"></uni-file-picker>
		<textarea name="text-content" id="text-content" cols="30" rows="20" placeholder="写下你想说的话吧!"></textarea>
		<button class="post-button" type="primary">发布</button>
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
	.uni-file-picker {
		width: 750rpx;
		margin-bottom: 50rpx;
	}
	
	#text-content {
		width: 750rpx;
		padding: 50rpx;
		border-top: 1px solid lightgray;
		border-bottom: 1px solid lightgray;
		margin-bottom: 50rpx;
		
		box-sizing: border-box;
	}
	
	.post-button {
		width: 500rpx;
		margin: auto;
	}
</style>
