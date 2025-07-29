<template>
  <div class="flex items-center justify-center" :class="containerClass">
    <div class="relative">
      <div 
        :class="spinnerClass"
        class="animate-spin rounded-full border-b-2"
      ></div>
      <div v-if="text" class="mt-2 text-sm text-gray-600 text-center">
        {{ text }}
      </div>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  size: {
    type: String,
    default: 'md',
    validator: (value) => ['sm', 'md', 'lg'].includes(value)
  },
  text: {
    type: String,
    default: ''
  },
  containerClass: {
    type: String,
    default: 'p-8'
  }
})

const spinnerClass = computed(() => {
  const sizeClasses = {
    sm: 'h-6 w-6 border-blue-600',
    md: 'h-8 w-8 border-blue-600',
    lg: 'h-12 w-12 border-blue-600'
  }
  return sizeClasses[props.size]
})
</script>
