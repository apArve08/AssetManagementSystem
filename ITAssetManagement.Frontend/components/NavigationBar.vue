<template>
  <nav class="bg-blue-600 text-white shadow-lg">
    <div class="container mx-auto px-4">
      <div class="flex justify-between items-center h-16">
        <div class="flex items-center space-x-8">
          <NuxtLink to="/" class="text-xl font-bold">IT Asset Management</NuxtLink>
          <div v-if="authStore.isAuthenticated" class="flex space-x-4">
            <NuxtLink to="/dashboard" class="hover:text-blue-200">Dashboard</NuxtLink>
            <NuxtLink to="/assets" class="hover:text-blue-200">Assets</NuxtLink>
          </div>
        </div>
        
        <div v-if="authStore.isAuthenticated" class="flex items-center space-x-4">
          <span class="text-sm">
            {{ authStore.currentUser?.fullName }} ({{ authStore.currentUser?.role }})
          </span>
          <button 
            @click="logout" 
            class="bg-blue-700 hover:bg-blue-800 px-3 py-1 rounded text-sm"
          >
            Logout
          </button>
        </div>
      </div>
    </div>
  </nav>
</template>

<script setup>
const authStore = useAuthStore()

const logout = () => {
  authStore.logout()
}
</script>
