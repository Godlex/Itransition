/**
 * Data Tree Progress Bar Handler
 * Manages progress bar display and button styling for Epic items
 */

$(document).ready(function() {
    
    /**
     * Remove button from Epic level items (level 0)
     */
    function removeButton() {
        $('li.wt-lp-datatree-item[wt-level="0"]')
            .children('.wt-lp-datatree-item-container')
            .find('button[wt-role="item-btn"]')
            .remove();
    }
     
    /**
     * Do not hide dates in subtasks - keep them visible
     */
    function hideItemText() {
        // Dates should remain visible in subtasks - no hiding needed
    }

    /**
     * Create and initialize progress bars for Epic level items
     */
    function createProgressBar() {
        $('li.wt-lp-datatree-item[wt-level="0"]').each(function () {
            let $item = $(this);
            let $subheader2 = $item.find('.wt-lp-datatree-item-subheader2');

            // Skip if already initialized
            if ($subheader2.data('progress-initialized')) return;

            // Parse progress value from subheader2
            let value = parseFloat($subheader2.text().trim());
            if (isNaN(value)) return;

            // If value > 1 (more than 100%), hide progress bar but keep Epic item
            if (value > 1) {
                $subheader2.data('progress-initialized', true).hide();
                return;
            }

            // Calculate percentage
            let percent = Math.round(value * 100);

            // Clear any existing inline styles that might interfere
            $subheader2.attr('style', '');
            
            // Mark as initialized and replace subheader2 content with progress bar
            $subheader2
                .data('progress-initialized', true)
                .css({
                    'display': 'inline-flex',
                    'flex-direction': 'row',
                    'align-items': 'center',
                    'gap': '8px',
                    'width': 'auto',
                    'height': 'auto',
                    'background': 'transparent',
                    'padding': '0',
                    'padding-bottom': '0',
                    'padding-left': '1em',
                    'margin': '0',
                    'font-family': 'Roboto, Arial, sans-serif',
                    'text-align': 'left',
                    'vertical-align': 'middle'
                })
                .empty()
                .append(`
                    <div class="wt-progress-text">${percent}%</div>
                    <div class="wt-progress-bar-container">
                        <div class="wt-progress-bar"></div>
                    </div>
                `);

            // Set progress bar width (cap at 100%)
            let barWidth = Math.min(percent, 100);
            $subheader2.find('.wt-progress-bar').css('width', barWidth + '%');
        });
    }

    /**
     * Initialize all modifications
     */
    function initializeAll() {
        removeButton();
        hideItemText();
        createProgressBar();
    }

    // 1. Initial execution
    initializeAll();

    // 2. Setup MutationObserver to handle dynamic content changes
    var observer = new MutationObserver(function(mutationsList) {
        initializeAll();
    });

    // Observe entire document body for changes
    observer.observe(document.body, { 
        childList: true, 
        subtree: true 
    });
    
});
