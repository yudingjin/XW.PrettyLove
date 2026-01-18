import {
    clearStorageAsync,
    getStorageAsync,
    removeStorageAsync,
    setStorageAsync
} from "./utils/storage"

App({
    async onShow() {
        setStorageAsync('name', 'lingshu').then(res => {
            console.log(res)
        })
        const res = await getStorageAsync('name')
        console.log(res.data)
        removeStorageAsync('name').then(res => {
            console.log(res)
        })
        clearStorageAsync().then(res => {
            console.log(res)
        })
    }
})