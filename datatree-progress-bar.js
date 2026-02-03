/**
 * Data Tree Progress Bar Handler
 * Manages progress bar display and button removal for Epic items
 */

$(document).ready(function() {
    
    /**
     * Remove "Изменить" button from Epic level items (level 0)
     */
    function removeButton() {
        $('li.wt-lp-datatree-item[wt-level="0"]')
            .children('.wt-lp-datatree-item-container')
            .find('button[wt-role="item-btn"]')
            .remove();
    }
     
    /**
     * Hide item-text in subtasks (level 1 and deeper)
     */
    function hideItemText() {
        // Hide progress in subtasks
        $('li.wt-lp-datatree-item[wt-level="1"] .wt-lp-datatree-item-text').hide();
        
        // Hide items with specific text content
        $('li.wt-lp-datatree-item .wt-lp-datatree-item-text').each(function() {
            if ($(this).text().trim() === "2") {
                $(this).hide();
            }
        });
    }

    /**
     * Create and initialize progress bars for Epic level items
     */
    function createProgressBar() {
        $('li.wt-lp-datatree-item[wt-level="0"]').each(function () {
            let $text = $(this).find('.wt-lp-datatree-item-text');

            // Skip if already initialized
            if ($text.data('progress-initialized')) return;

            // Parse progress value
            let value = parseFloat($text.text());
            if (isNaN(value)) return;

            // Calculate percentage
            let percent = Math.round(value * 100);

            // Mark as initialized and update HTML
            $text
                .data('progress-initialized', true)
                .empty()
                .append(`
                    <div class="wt-progress-text">${percent}%</div>
                    <div class="wt-progress-bar"></div>
                `);

            // Set progress bar width
            $text.find('.wt-progress-bar').css('width', percent + '%');
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
