import { defineStore } from 'pinia'

export const useAuthStore = defineStore('auth', {
state: () => ({
user: null,
token: null,
isAuthenticated: false
}),

getters: {
isAdmin: (state) => state.user?.role === 'Admin',
currentUser: (state) => state.user
},

actions: {
setAuth(authData) {
this.user = {
username: authData.username,
role: authData.role,
fullName: authData.fullName
}
this.token = authData.token
this.isAuthenticated = true

if (process.client) {
localStorage.setItem('auth',JSON.stringify({
user: this.user,
token: this.token
}))
}
},

loadAuth() {
if (process.client) {
const stored = localStorage.getItem('auth')
if (stored) {
const { user, token } = JSON.parse(stored)
this.user = user
this.token = token
this.isAuthenticated = true
}
}
},

logout() {
this.user = null
this.token = null
this.isAuthenticated = false

if (process.client) {
localStorage.removeItem('auth')
}

navigateTo('/login')
}
}
})
