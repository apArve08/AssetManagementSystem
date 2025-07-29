<template>
  <div>
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-3xl font-bold text-gray-900">Assets</h1>
      <button class="bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded">
        Add Asset
      </button>
    </div>
    
    <div class="bg-white shadow-lg rounded-lg overflow-hidden">
      <div class="p-8 text-center text-gray-500">
        No assets found. Add your first asset to get started.
      </div>
    </div>
  </div>
</template>

<script setup>
definePageMeta({
  middleware: 'auth'
})
</script>
