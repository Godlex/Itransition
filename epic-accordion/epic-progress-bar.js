/**
 * Epic Accordion Progress Bar
 * 
 * This script handles:
 * - Removing buttons from Epic level items (level 0)
 * - Hiding subheader2 in subtasks (level 1+)
 * - Creating and initializing progress bars for Epic level items
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
     * Hide subheader2 in subtasks and if value equals 2
     */
    function hideSubheader2() {
        // Hide subheader2 in subtasks (level 1+)
        $('li.wt-lp-datatree-item[wt-level="1"] .wt-lp-datatree-item-subheader2').hide();
        $('li.wt-lp-datatree-item[wt-level="2"] .wt-lp-datatree-item-subheader2').hide();
        $('li.wt-lp-datatree-item[wt-level="3"] .wt-lp-datatree-item-subheader2').hide();
        
        // Hide subheader2 if numeric value equals 2 (200%)
        $('li.wt-lp-datatree-item[wt-level="0"] .wt-lp-datatree-item-subheader2').each(function() {
            let value = parseFloat($(this).text().trim());
            if (!isNaN(value) && value === 2) {
                $(this).hide();
            }
        });
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
            
            // Check if subheader2 contains wt-progress-text (already transformed)
            if ($subheader2.find('.wt-progress-text').length > 0) return;

            // Parse progress value from subheader2 ORIGINAL text
            let originalText = $subheader2.text().trim();
            let value = parseFloat(originalText);
            if (isNaN(value)) return;

            // If value equals 2 (200%), hide and don't create progress bar
            if (value === 2) {
                $subheader2.data('progress-initialized', true).hide();
                return;
            }

            // Calculate percentage
            let percent = Math.round(value * 100);

            // Clear any existing inline styles that might interfere
            $subheader2.attr('style', '');
            
            // Mark as initialized and replace subheader2 content with progress bar (vertical layout)
            $subheader2
                .data('progress-initialized', true)
                .css({
                    'display': 'flex',
                    'flex-direction': 'column',
                    'align-items': 'center',
                    'justify-content': 'center',
                    'gap': '4px',
                    'width': 'auto',
                    'min-width': '200px',
                    'height': 'auto',
                    'background': 'transparent',
                    'padding': '8px 0',
                    'padding-left': '1em',
                    'margin': '0',
                    'font-family': 'Roboto, Arial, sans-serif',
                    'text-align': 'center'
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
        hideSubheader2();
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
