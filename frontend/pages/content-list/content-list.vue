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
			
		}
	}
</script>

<style>

</style>
