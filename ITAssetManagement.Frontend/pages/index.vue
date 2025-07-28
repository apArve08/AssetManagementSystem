
<template>
  <div class="p-6 text-center">
    <p class="text-gray-500 text-lg">Redirecting to dashboard...</p>
  </div>
</template>
  <script setup>
import { navigateTo } from 'nuxt/app';
import { onMounted } from 'vue';

  
  definePageMeta({
    middleware: 'auth'
  })
  onMounted(() => {
    navigateTo('/dashboard')
  })
  </script>

