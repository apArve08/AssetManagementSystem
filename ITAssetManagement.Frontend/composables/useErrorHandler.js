export const useErrorHandler = () => {
  const { $toast } = useNuxtApp()

  const handleError = (error, fallbackMessage = 'An error occurred') => {
    console.error('Error:', error)
    
    let message = fallbackMessage
    
    if (error.response?.data?.message) {
      message = error.response.data.message
    } else if (error.message) {
      message = error.message
    }
    
    $toast.error(message)
  }

  const handleSuccess = (message) => {
    $toast.success(message)
  }

  return {
    handleError,
    handleSuccess
  }
}
