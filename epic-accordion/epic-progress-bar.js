/**
 * Epic Accordion Progress Bar
 * 
 * Creates and styles progress bars for Epic level items (level 0)
 */
$(document).ready(function() {
    
    /**
     * Remove "Изменить" button from Epic level items (level 0)
     */
    function removeEpicButtons() {
        $('li.wt-lp-datatree-item[wt-level="0"]')
            .children('.wt-lp-datatree-item-container')
            .find('button[wt-role="item-btn"]')
            .remove();
    }
     
    /**
     * Hide subheader2 in subtasks (level 1+)
     */
    function hideSubtaskSubheader2() {
        $('li.wt-lp-datatree-item[wt-level="1"] .wt-lp-datatree-item-subheader2').hide();
        $('li.wt-lp-datatree-item[wt-level="2"] .wt-lp-datatree-item-subheader2').hide();
        $('li.wt-lp-datatree-item[wt-level="3"] .wt-lp-datatree-item-subheader2').hide();
    }

    /**
     * Apply styles to progress bar element
     */
    function styleProgressBar($bar, widthPercent) {
        $bar.attr('style', [
            'height: 100%',
            'width: ' + widthPercent + '%',
            'background: linear-gradient(90deg, #FFA726 0%, #FF9800 50%, #F57C00 100%)',
            'border-radius: 4px',
            'transition: width 0.3s ease'
        ].join('; '));
    }

    /**
     * Apply styles to progress bar container
     */
    function styleProgressContainer($container) {
        $container.attr('style', [
            'width: 200px',
            'height: 8px',
            'background: #E0E0E0',
            'border-radius: 4px',
            'overflow: hidden',
            'position: relative'
        ].join('; '));
    }

    /**
     * Create progress bar HTML
     */
    function createProgressBarHTML(percent) {
        let barWidth = Math.min(percent, 100);
        return `
            <div class="wt-progress-wrapper" style="display: flex; flex-direction: column; align-items: center; gap: 8px; min-width: 200px; padding: 8px 0;">
                <div class="wt-progress-text" style="font-size: 14px; font-weight: 500; color: #666666; text-align: center;">${percent}%</div>
                <div class="wt-progress-bar-container" style="width: 200px; height: 8px; background: #E0E0E0; border-radius: 4px; overflow: hidden; position: relative;">
                    <div class="wt-progress-bar" style="height: 100%; width: ${barWidth}%; background: linear-gradient(90deg, #FFA726 0%, #FF9800 50%, #F57C00 100%); border-radius: 4px;"></div>
                </div>
            </div>
        `;
    }

    /**
     * Initialize progress bars for Epic level items
     */
    function initProgressBars() {
        $('li.wt-lp-datatree-item[wt-level="0"]').each(function() {
            let $subheader2 = $(this).find('.wt-lp-datatree-item-subheader2');
            if ($subheader2.length === 0) return;

            // Case 1: Progress bar already exists - just apply styles
            let $existingBar = $subheader2.find('.wt-progress-bar');
            if ($existingBar.length > 0) {
                // Get width from existing inline style
                let existingStyle = $existingBar.attr('style') || '';
                let widthMatch = existingStyle.match(/width:\s*(\d+)/);
                let widthPercent = widthMatch ? parseInt(widthMatch[1]) : 0;
                
                // Apply styles to container and bar
                let $container = $subheader2.find('.wt-progress-bar-container');
                styleProgressContainer($container);
                styleProgressBar($existingBar, widthPercent);
                return;
            }

            // Case 2: Need to create progress bar from value
            // Skip if already has wrapper (fully initialized)
            if ($subheader2.find('.wt-progress-wrapper').length > 0) return;
            
            // Get original text value
            let originalText = $subheader2.text().trim();
            let value = parseFloat(originalText);
            
            // Skip if not a valid number
            if (isNaN(value)) return;
            
            // Hide if value is 2 (200% = hidden)
            if (value === 2) {
                $subheader2.hide();
                return;
            }

            // Calculate percentage
            let percent = Math.round(value * 100);
            
            // Replace content with progress bar
            $subheader2
                .empty()
                .append(createProgressBarHTML(percent))
                .css({
                    'display': 'flex',
                    'align-items': 'center',
                    'justify-content': 'center'
                });
        });
    }

    /**
     * Initialize all modifications
     */
    function initializeAll() {
        removeEpicButtons();
        hideSubtaskSubheader2();
        initProgressBars();
    }

    // Run on page load
    initializeAll();

    // Watch for dynamic content changes
    var observer = new MutationObserver(function() {
        initializeAll();
    });

    observer.observe(document.body, { 
        childList: true, 
        subtree: true 
    });
});
