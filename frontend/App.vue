<script>
	export default {
		onLaunch: function() {
			console.log('App Launch')
		},
		onShow: function() {
			console.log('App Show')
		},
		onHide: function() {
			console.log('App Hide')
		},
		globalData:{
			toDetailContentId:0,//用于表示去详情时文章id
			toCommentId:0,//用于表示去评价时评论所属文章的id
			userToWhitch:100,//用于标识要去粉丝列表（1）还是关注列表（2）
			toUserlistId:0,//用于表示前往用户表时当前用户id
			toOtherUserId:0,//用于表示前往其他用户主页时那个用户的Id
			toContentListId:0,//用于表示前往内容表时当前用户的id
	
		}
	}
</script>

<style>
	/*每个页面公共css */
</style>
