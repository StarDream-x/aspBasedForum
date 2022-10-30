<template>
	<view class="card-list">
		<ContentCard title="一个一个一个一个一个标题" author="一个一个一个一个一个一个一个一个作者" viewCount="一个阅读量" imageSrc="/static/logo.png" />
		<ContentCard title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
		<ContentCard title="另一个标题" author="另一个作者" viewCount="另一个" imageSrc="/static/logo.png" />
	</view>
</template>

<script>
	import ContentCard from '~@/components/ContentCard.vue'
	export default {
		components: {
			ContentCard
		},
		data() {
			return {
				
			}
		},
		methods: {
			
		}
	}
</script>

<style>
	.card-list {
		display: grid;
		grid-template-columns: 375rpx 375rpx;
		row-gap: 10rpx;
		align-items: center;
		justify-items: center;
	}
</style>
