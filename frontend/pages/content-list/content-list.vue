<template>
	<view class="content-list">
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
		<ContentListItem :viewCount="114" author="作者" title="标题" imageSrc="/static/logo.png" />
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
			getList(){
				//TODO:获取用户收藏的文章列表
			}
		},
		onLoad: go(options){
			this.event = this.getOpenerEventChannel()
			//接收页面1传来的数据
			this.event.on('id', (res) => {
				this.userId = res
			})
			getList();
		}
	}
</script>

<style>

</style>
