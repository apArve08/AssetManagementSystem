<template>
  <div>
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-3xl font-bold text-gray-900">Assets</h1>
      <div class="flex space-x-4">
        <button
          @click="exportAssets"
          class="bg-green-600 hover:bg-green-700 text-white font-medium py-2 px-4 rounded"
        >
          Export to Excel
        </button>
        <button
          @click="showAddForm = true"
          class="bg-blue-600 hover:bg-blue-700 text-white font-medium py-2 px-4 rounded"
        >
          Add Asset
        </button>
      </div>
    </div>

    <!-- Search Bar -->
    <div class="mb-6">
      <input
        v-model="searchQuery"
        type="text"
        placeholder="Search assets..."
        class="w-full px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
        @input="searchAssets"
      />
    </div>

    <!-- Asset List -->
    <AssetList 
      :assets="assets" 
      @edit="editAsset" 
      @delete="deleteAsset"
      @view="viewAsset"
    />

    <!-- Add/Edit Modal -->
    <div v-if="showAddForm || editingAsset" class="fixed inset-0 bg-gray-600 bg-opacity-50 overflow-y-auto h-full w-full z-50">
      <div class="relative top-20 mx-auto p-5 border w-full max-w-2xl shadow-lg rounded-md bg-white">
        <h3 class="text-lg font-bold text-gray-900 mb-4">
          {{ editingAsset ? 'Edit Asset' : 'Add New Asset' }}
        </h3>
        <AssetForm 
          :asset="editingAsset"
          @save="saveAsset"
          @cancel="cancelForm"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
import AssetList from '~/components/AssetList.vue'
import AssetForm from '~/components/AssetForm.vue'

definePageMeta({
  middleware: 'auth'
})

const { $api, $toast } = useNuxtApp()
const authStore = useAuthStore()

const assets = ref([])
const searchQuery = ref('')
const showAddForm = ref(false)
const editingAsset = ref(null)

const loadAssets = async () => {
  try {
    const { data } = await $api.get('/assets')
    assets.value = data
  } catch (error) {
    $toast.error('Failed to load assets')
  }
}

const searchAssets = async () => {
  try {
    const { data } = await $api.get('/assets', {
      params: { search: searchQuery.value }
    })
    assets.value = data
  } catch (error) {
    $toast.error('Search failed')
  }
}

const editAsset = (asset) => {
  editingAsset.value = { ...asset }
}

const viewAsset = (asset) => {
  navigateTo(`/assets/${asset.id}`)
}

const saveAsset = async (assetData) => {
  try {
    if (editingAsset.value) {
      await $api.put(`/assets/${editingAsset.value.id}`, assetData)
      $toast.success('Asset updated successfully')
    } else {
      await $api.post('/assets', assetData)
      $toast.success('Asset created successfully')
    }
    
    showAddForm.value = false
    editingAsset.value = null
    await loadAssets()
  } catch (error) {
    $toast.error('Failed to save asset')
  }
}

const deleteAsset = async (asset) => {
  if (!authStore.isAdmin) {
    $toast.error('Only administrators can delete assets')
    return
  }

  if (confirm(`Are you sure you want to delete ${asset.assetTag}?`)) {
    try {
      await $api.delete(`/assets/${asset.id}`)
      $toast.success('Asset deleted successfully')
      await loadAssets()
    } catch (error) {
      $toast.error('Failed to delete asset')
    }
  }
}

const cancelForm = () => {
  showAddForm.value = false
  editingAsset.value = null
}

const exportAssets = async () => {
  try {
    const response = await $api.get('/export/assets', {
      responseType: 'blob'
    })
    
    const url = window.URL.createObjectURL(new Blob([response.data]))
    const link = document.createElement('a')
    link.href = url
    link.setAttribute('download', `Assets_Export_${new Date().toISOString().split('T')[0]}.xlsx`)
    document.body.appendChild(link)
    link.click()
    link.remove()
    
    $toast.success('Assets exported successfully')
  } catch (error) {
    $toast.error('Failed to export assets')
  }
}

onMounted(() => {
  loadAssets()
})
</script>
