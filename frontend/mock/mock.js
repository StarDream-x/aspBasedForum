import Mock from 'mockjs'

export default function() {
	Mock.mock('http://localhost:3000/login', {
		"token": "@datetime"
	})
	
	Mock.mock(/checkId/, {
			"valid": true
		})
	Mock.mock('http://localhost:3000/register', {
		"token": "@datetime"
	})
	
	Mock.mock(/content\?*prevRequest/, {
		requestId: "@datetime",
		"contents|1-10": [
			{
				"id|+1": "1",
				title:"标题1",
				authorId: "fsdgsdfg",
				authorName: "@cname",
				viewCount: 4567,
				imageUrl:'/static/logo.png'
			}
		]
	})
	
	Mock.mock(/content\/[0-9]+\/interaction$/, {
		"favorite|1-2": true,
		"like|1-2": true
	})
	
	Mock.mock(/content\/[0-9]+$/, {
		"id|1-100": 1,
		"title": "@cname",
		"authorId": "3f3gt4",
		"viewCount|1-1000": 1,
		"likeCount|1-999": 1,
		"favoriteCount|1-999": 1,
		publishTime: 85876789768,
		body: "@cname",
		media: [
			{
				type: "picture",
				imageUrl: "/static/logo.png",
				url: "/static/logo.png"
			},
			{
				type: "picture",
				imageUrl: "/static/logo.png",
				url: "/static/logo.png"
			},
			{
				type: "picture",
				imageUrl: "/static/logo.png",
				url: "/static/logo.png"
			},
			{
				type: "picture",
				imageUrl: "/static/logo.png",
				url: "/static/logo.png"
			},
			{
				type: "picture",
				imageUrl: "/static/logo.png",
				url: "/static/logo.png"
			}
		],
		comments: [
			{						
				"id|1-100": 1,
				user: {
					id: "6478659",
					avatarUrl: "/static/logo.png",
					username: "@cname",
					introduction: "介绍",
					"followingCount|1-1000": 1,
					"followerCount|1-1000": 1,
					"favoriteCount|1-1000": 1
				},
				content: "@cname",
				"likeCount|1-100": 1,
				"liked|1-2": true,
				"publishTime": 458453683475,
				"like|1-2": true
			},
			{						
				"id|1-100": 1,
				user: {
					id: "6478659",
					avatarUrl: "/static/logo.png",
					username: "@cname",
					introduction: "介绍",
					"followingCount|1-1000": 1,
					"followerCount|1-1000": 1,
					"favoriteCount|1-1000": 1
				},
				content: "@cname",
				"likeCount|1-100": 1,
				"liked|1-2": true,
				"publishTime": 458453683475,
				"like|1-2": true
			},
			{						
				"id|1-100": 1,
				user: {
					id: "6478659",
					avatarUrl: "/static/logo.png",
					username: "@cname",
					introduction: "介绍",
					"followingCount|1-1000": 1,
					"followerCount|1-1000": 1,
					"favoriteCount|1-1000": 1
				},
				content: "@cname",
				"likeCount|1-100": 1,
				"liked|1-2": true,
				"publishTime": 458453683475,
				"like|1-2": true
			},
			{						
				"id|1-100": 1,
				user: {
					id: "6478659",
					avatarUrl: "/static/logo.png",
					username: "@cname",
					introduction: "介绍",
					"followingCount|1-1000": 1,
					"followerCount|1-1000": 1,
					"favoriteCount|1-1000": 1
				},
				content: "@cname",
				"likeCount|1-100": 1,
				"liked|1-2": true,
				"publishTime": 458453683475,
				"like|1-2": true
			},
		]
	})
	
	Mock.mock(/user\/[a-zA-Z0-9]+$/, {
		id: "userID",
		avatarUrl: "/static/logo.png",
		username: "@cname",
		introduction: "介绍",
		"followingCount|1-1000": 1,
		"followerCount|1-1000": 1,
		"favoriteCount|1-1000": 1
	})
	
	Mock.mock(/content\/[0-9]+\/interaction$/, {
		"like|1-2": true,
		"favorite|1-2": true
	})
	
	Mock.mock(/content\/[0-9]+\/comment\/[0-9]+$/, {
		"like|1-2": true
	})
	
	Mock.mock(/(content\?*userId)|(user\/[a-zA-Z0-9_]+\/favorite$)/, [
		{
			"id|1-100": 1,
			title:"标题1",
			authorId: "fsdgsdfg",
			authorName: "@cname",
			viewCount: 4567,
			imageUrl:'/static/logo.png'
		},
		{
				"id|1-100": 1,
				title:"标题1",
				authorId: "fsdgsdfg",
				authorName: "@cname",
				viewCount: 4567,
				imageUrl:'/static/logo.png'
		},
		{
				"id|1-100": 1,
				title:"标题1",
				authorId: "fsdgsdfg",
				authorName: "@cname",
				viewCount: 4567,
				imageUrl:'/static/logo.png'
		},
		{
			"id|1-100": 1,
			title:"标题1",
			authorId: "fsdgsdfg",
			authorName: "@cname",
			viewCount: 4567,
			imageUrl:'/static/logo.png'
		},
		{
				"id|1-100": 1,
				title:"标题1",
				authorId: "fsdgsdfg",
				authorName: "@cname",
				viewCount: 4567,
				imageUrl:'/static/logo.png'
		},
		{
				"id|1-100": 1,
				title:"标题1",
				authorId: "fsdgsdfg",
				authorName: "@cname",
				viewCount: 4567,
				imageUrl:'/static/logo.png'
		}
	]);
	
	Mock.mock(/user\/[a-zA-z0-9_]+\/(follower|following)$/, [
		{
			id: "id",
			avatarUrl: "/static/logo.png",
			username: 'username',
			introduction: "introduction",
			following: false
		},
		{
			id: "id",
			avatarUrl: "/static/logo.png",
			username: 'username',
			introduction: "introduction",
			following: false
		},
		{
			id: "id",
			avatarUrl: "/static/logo.png",
			username: 'username',
			introduction: "introduction",
			following: false
		},
		{
			id: "id",
			avatarUrl: "/static/logo.png",
			username: 'username',
			introduction: "introduction",
			following: false
		},
		{
			id: "id",
			avatarUrl: "/static/logo.png",
			username: 'username',
			introduction: "introduction",
			following: false
		},
		{
			id: "id",
			avatarUrl: "/static/logo.png",
			username: 'username',
			introduction: "introduction",
			following: false
		},
	])
	
	Mock.mock(/me\/follow/, {
		"following|1-2": true
	})
}