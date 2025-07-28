export default defineNuxtRouteMiddleware(async (to, from) => {
    const authStore = useAuthStore()
  
    if (process.client) {
      await authStore.loadAuth()
    }
  
    if (!authStore.isAuthenticated && to.path !== '/login') {
      return navigateTo('/login')
    }
})