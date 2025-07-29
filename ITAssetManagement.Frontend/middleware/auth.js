export default defineNuxtRouteMiddleware((to, from) => {
  const authStore = useAuthStore()
  
  // Load auth from localStorage on first load
  if (!authStore.isAuthenticated && process.client) {
    authStore.loadAuth()
  }
  
  if (!authStore.isAuthenticated) {
    return navigateTo('/login')
  }
})
