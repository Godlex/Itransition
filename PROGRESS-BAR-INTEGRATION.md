# Data Tree Progress Bar Integration Guide

## Overview
This solution provides custom CSS and JavaScript to display progress bars on Epic-level items in a data tree structure, while hiding them in subtasks.

## Files Created
1. **datatree-progress-bar.css** - Styling for the progress bars
2. **datatree-progress-bar.js** - JavaScript logic for progress bar initialization

## Features
- ✨ Progress bars displayed only at Epic level (wt-level="0")
- 🎨 Orange gradient progress bar (#FF9800 → #F57C00)
- 📊 Compact horizontal progress bar layout (percentage + bar inline)
- 🔄 Automatic initialization via MutationObserver
- 🗑️ Removes button completely from Epic items
- 📋 Keeps "Подзадачи" (Task list) dropdown unchanged
- 👁️ Hides progress bars in subtasks (level 1+)
- 🚫 Automatically hides Epic items with progress > 100% (value > 1)
- 💳 Clean, compact Epic card styling matching reference design

## Integration Steps

### 1. Include CSS File
Add the CSS file to your HTML page:

```html
<link rel="stylesheet" href="datatree-progress-bar.css">
```

Or inject it dynamically:

```javascript
var link = document.createElement('link');
link.rel = 'stylesheet';
link.href = '/path/to/datatree-progress-bar.css';
document.head.appendChild(link);
```

### 2. Include JavaScript File
Ensure jQuery is loaded first, then add the JS file:

```html
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="datatree-progress-bar.js"></script>
```

### 3. Data Format
The progress value should be stored as a decimal in the `wt-lp-datatree-item-text` element:
- `0.4` = 40%
- `0.5` = 50%
- `1.0` = 100%
- `2.0` = 200% (Epic item will be **hidden automatically**)

**Important:** Epic items with progress > 100% (value > 1) are automatically hidden.

Example in HTML:
```html
<div class="wt-lp-datatree-item-text" wt-role="item-text">0.4</div>
```

After initialization, it becomes:
```html
<div class="wt-lp-datatree-item-text" wt-role="item-text" style="display: inline-flex;">
    <div class="wt-progress-text">40%</div>
    <div class="wt-progress-bar-container">
        <div class="wt-progress-bar" style="width: 40%;"></div>
    </div>
</div>
```

The button in Epic items is **automatically removed** (not changed, but deleted).

## Customization

### Change Progress Bar Colors
Edit `datatree-progress-bar.css`:

```css
.wt-progress-bar {
    background: linear-gradient(90deg, #YOUR_START_COLOR 0%, #YOUR_END_COLOR 100%) !important;
}
```

### Change Progress Bar Size
Edit `datatree-progress-bar.css`:

```css
.wt-progress-bar-container {
    width: 200px !important;  /* Change width */
    height: 4px !important;   /* Change height */
}
```

### Show Items with Progress > 100%
Comment out the hiding logic in `datatree-progress-bar.js`:

```javascript
function createProgressBar() {
    // ...
    
    // Comment out these lines to show items with >100% progress
    // if (value > 1) {
    //     $item.hide();
    //     return;
    // }
    
    // ...
}
```

### Keep Button in Epic Items
Comment out the removeButton call in `datatree-progress-bar.js`:

```javascript
function initializeAll() {
    // removeButton();  // <-- Comment this out
    hideItemText();
    createProgressBar();
}
```

## Browser Support
- Chrome/Edge: ✅
- Firefox: ✅
- Safari: ✅
- IE11: ⚠️ (requires polyfills for MutationObserver)

## Dependencies
- jQuery 1.7+

## Troubleshooting

### Progress bars not appearing
1. Check that jQuery is loaded before the script
2. Verify the element has `wt-level="0"` attribute
3. Ensure the text content is a valid number

### Progress bars appearing in subtasks
1. Check that subtasks have `wt-level="1"` or higher
2. Verify CSS file is loaded properly

### Progress not updating dynamically
- The MutationObserver should handle this automatically
- Check browser console for JavaScript errors

## Technical Details

### HTML Structure
The script expects this structure:
```html
<li class="wt-lp-datatree-item" wt-level="0">
    <div class="wt-lp-datatree-item-container">
        <div class="wt-lp-datatree-item-body">
            <div class="wt-lp-datatree-item-content">
                <div class="wt-lp-datatree-item-text">0.5</div>
            </div>
        </div>
    </div>
</li>
```

### Key CSS Classes
- `.wt-lp-datatree-item-text` - Progress container
- `.wt-progress-bar` - Colored progress bar
- `.wt-progress-text` - Percentage label

### Key Functions
- `removeButton()` - Removes buttons completely from Epic items
- `hideItemText()` - Hides progress in subtasks
- `createProgressBar()` - Initializes compact inline progress bars and hides items >100%
- `initializeAll()` - Runs all initialization functions

## License
This code is provided as-is for integration with your data tree system.
