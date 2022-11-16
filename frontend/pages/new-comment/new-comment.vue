<template>
	<view class="new-comment">
		<textarea name="comment" id="comment" cols="30" rows="10" placeholder="评论内容..." v-model="this.text"></textarea>
		<button class="button-submit" type="primary" @click="release">发布</button>
	</view>
</template>

<script>
	import {myRequest} from '~@/util/api.js'
	export default {
		data() {
			return {
				contentsId:0,
				text:''
			}
		},
		methods: {
			release(){
				myRequest({
					url:"content/"+this.contentsId+"/comment",
					method:'POST',
					data:{
						content:this.text
					}
				}).then((res) => {
					if(res.statusCode == 200){
						getApp().globalData.toDetailContentId = this.contentsId
						uni.navigateBack()
						uni.showToast({
							icon: "success",
							title: "发布成功！"
						})
					}
				})
			}
			
		},
		onLoad(options){
			this.contentsId = getApp().globalData.toCommentId
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
