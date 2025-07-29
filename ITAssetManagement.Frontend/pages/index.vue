<template>
  <div></div>
</template>

<script setup>
definePageMeta({
  middleware: 'auth'
})

await navigateTo('/dashboard')
</script>
