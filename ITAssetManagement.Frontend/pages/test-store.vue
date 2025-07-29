<template>
<div class="p-8">
<h1 class="text=2xl font-bold mb-4">Store Test</h1>
<p>Is Authenticated: {{ authStore.isAuthenticated }}</p>
<p>User: {{ authStore.user || 'Not logged in' }}</p>
<button @click="testLogin" class="mt-4 bg-green-500 text-white px-4 py-2 rounded">
Test Login
</button>
<button @click="authStore.logout()" class="mt-4 ml-2 bg-red-500 text-white px-4 py-2 rounded">
Test Logout
</button>
</div>
</template>

<script setup>
const authStore = useAuthStore()

const testLogin = () => {
authStore.setAuth({
username: 'testuser',
role: 'Admin',
fullName: 'Test User',
token: 'fake-token'
})
}
</script>
