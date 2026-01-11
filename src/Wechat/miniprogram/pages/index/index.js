// index.js
Page({
  data: {
    avatarUrl: '../../assets/left.jpg',
    nickname: ''
  },
  //获取微信头像
  chooseavator(event) {
    //目前获取的微信头像是临时路径
    //临时路径是有失效时间的，在实际开发中，需要将临时路径上传到公司服务器
    const { avatarUrl } = event.detail
    console.log(event)
    this.setData({
      avatarUrl: avatarUrl
    })
  },
  //获取微信昵称
  onSubmit(event) {
    const { nickname } = event.detail.value
    this.setData({
      nickname: nickname
    })
  },

  //用来监听页面按钮的转发以及右上角的转发按钮
  onShareAppMessage(obj) {
    console.log(obj)
    return {
      title: '这是一个神奇的页面',
      path: '/pages/index/index',
      imageUrl: '../../assets/right.jpg'
    }
  },

  //监听右上角 分享到朋友圈 按钮
  onShareTimeline(){
    return {
      title: '帮我砍一刀',
      query:'id=1',
      imageUrl: '../../assets/right.jpg'
    }
  },

  //手机号快速验证
  getphonenumber(event){
    //通过事件对象 可以看到  在event.detail中可以获取到code
    //code 动态令牌。可以使用 code 获取用户的手机号
    //需要将code发送给后端，后端在接收到code 以后 也需要调用api 获取用户的真正手机号
    console.log(event)
  },

  //手机号实时验证
  getrealtimephonenumber(event){
    console.log(event)
  }
})