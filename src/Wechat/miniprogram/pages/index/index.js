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
  }
})