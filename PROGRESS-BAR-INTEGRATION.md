# Data Tree Progress Bar Integration Guide

## Overview
This solution provides custom CSS and JavaScript to display progress bars on Epic-level items in a data tree structure, while hiding them in subtasks.

## Files Created
1. **datatree-progress-bar.css** - Styling for the progress bars
2. **datatree-progress-bar.js** - JavaScript logic for progress bar initialization

## Features
- ✨ Progress bars displayed only at Epic level (wt-level="0")
- 🎨 Orange gradient progress bar (#FFA320 → #E26900)
- 📊 Horizontal progress bar layout (percentage + bar side by side)
- 🔄 Automatic initialization via MutationObserver
- 🔘 Changes "Изменить" button to "Подробнее" with orange styling
- 📋 Keeps "Подзадачи" (Task list) dropdown unchanged
- 👁️ Hides progress bars in subtasks (level 1+)
- 💳 Enhanced Epic card styling with shadows and borders

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
- `0.5` = 50%
- `1.0` = 100%
- `2.0` = 200% (will display as 200%)

Example in HTML:
```html
<div class="wt-lp-datatree-item-text" wt-role="item-text">0.5</div>
```

After initialization, it becomes:
```html
<div class="wt-lp-datatree-item-text" wt-role="item-text">
    <div class="wt-progress-text">50%</div>
    <div class="wt-progress-bar-container">
        <div class="wt-progress-bar" style="width: 50%;"></div>
    </div>
</div>
```

The "Изменить" button is automatically changed to:
```html
<button class="wt-lp-datatree-item-btn wt-lp-has-bg wt-details-btn" wt-role="item-btn">
    Подробнее
</button>
```

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
    width: 240px;  /* Change width */
    height: 8px;   /* Change height */
}
```

### Change Button Color
Edit `datatree-progress-bar.css`:

```css
li.wt-lp-datatree-item[wt-level="0"] button[wt-role="item-btn"].wt-details-btn {
    background: #YOUR_COLOR !important;
}

li.wt-lp-datatree-item[wt-level="0"] button[wt-role="item-btn"].wt-details-btn:hover {
    background: #YOUR_HOVER_COLOR !important;
}
```

### Change Button Text
Edit `datatree-progress-bar.js`:

```javascript
function updateButton() {
    // ... existing code ...
    $button.addClass('wt-details-btn').text('Your Custom Text');
}
```

### Disable Button Modification
Comment out the function call in `datatree-progress-bar.js`:

```javascript
function initializeAll() {
    // updateButton();  // <-- Comment this out
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
- `updateButton()` - Changes "Изменить" to "Подробнее" button for Epic items
- `hideItemText()` - Hides progress in subtasks
- `createProgressBar()` - Initializes horizontal progress bars
- `initializeAll()` - Runs all initialization functions

## License
This code is provided as-is for integration with your data tree system.
