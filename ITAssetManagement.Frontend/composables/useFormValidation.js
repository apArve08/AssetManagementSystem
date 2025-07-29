export const useFormValidation = () => {
  const validateAsset = (asset) => {
    const errors = {}

    // Required fields
    if (!asset.name?.trim()) {
      errors.name = 'Asset name is required'
    }

    if (!asset.category?.trim()) {
      errors.category = 'Category is required'
    }

    if (!asset.serialNumber?.trim()) {
      errors.serialNumber = 'Serial number is required'
    }

    if (!asset.status?.trim()) {
      errors.status = 'Status is required'
    }

    // Conditional validation
    if (asset.status === 'Assigned' && !asset.assignedTo?.trim()) {
      errors.assignedTo = 'Assigned To is required when status is Assigned'
    }

    // Format validation
    if (asset.purchasePrice && asset.purchasePrice < 0) {
      errors.purchasePrice = 'Purchase price cannot be negative'
    }

    // Date validation
    if (asset.warrantyExpiry && asset.purchaseDate) {
      const warranty = new Date(asset.warrantyExpiry)
      const purchase = new Date(asset.purchaseDate)
      
      if (warranty < purchase) {
        errors.warrantyExpiry = 'Warranty expiry cannot be before purchase date'
      }
    }

    return {
      isValid: Object.keys(errors).length === 0,
      errors
    }
  }

  return {
    validateAsset
  }
}
