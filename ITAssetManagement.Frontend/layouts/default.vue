<script setup lang="ts">
import NavigationBar from '../components/NavigationBar.vue';

</script>

<template>
    <div class="min-h-screen bg-gray-50">
        <NavigationBar />
        <main class="container mx-auto px-4 py-8">
            <slot />
        </main>
    </div>
</template>

<script setup>
import NavigationBar from '~/components/NavigationBar.vue';

</script>