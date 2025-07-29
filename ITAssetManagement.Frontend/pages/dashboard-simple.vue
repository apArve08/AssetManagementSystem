<template>
  <div class="p-8">
    <h1 class="text-3xl font-bold mb-4">Dashboard</h1>
    <p class="mb-4">Welcome, {{ authStore.currentUser?.fullName }}!</p>
    <div class="space-x-4">
      <NuxtLink to="/assets-simple" class="text-blue-600 hover:underline">
        Go to Assets
      </NuxtLink>
      <button @click="authStore.logout()" class="text-red-600 hover:underline">
        Logout
      </button>
    </div>
  </div>
</template>

<script setup>
const authStore = useAuthStore()
</script>
