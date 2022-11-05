<template>
	<view class="new-comment">
		<textarea name="comment" id="comment" cols="30" rows="10" placeholder="评论内容...">{{text}}</textarea>
		<button class="button-submit" type="primary" @click="release">发布</button>
	</view>
</template>

<script>
	export default {
		data() {
			return {
				contentsId:null,
				text:''
			}
		},
		methods: {
			onload(){
				this.event = this.getOpenerEventChannel()
				//接收页面1传来的数据
				this.event.on('id', (res) => {
					this.contentsId = res
				})
			},
			release(){
				myRequest({
					url:"content/"+this.contentsId+"/comment",
					method:'POST',
					data:{
						content:this.text
					}
				}).then((res) => {
					if(res.statusCode == 200){
						uni.showToast({
							icon: "success",
							title: "发布成功！"
						})
						uni.navigateTo({
							url:"/pages/detail/detail",
							success: (res) => {
							res.eventChannel.emit('id',contents.id)
						}
						})
					}
				})
			}
			
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
