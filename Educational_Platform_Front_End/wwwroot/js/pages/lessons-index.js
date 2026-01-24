document.addEventListener('DOMContentLoaded', () => {
    const searchInput = document.getElementById('searchInput');
    const categoryFilter = document.getElementById('categoryFilter');
    const durationFilter = document.getElementById('durationFilter');
    const resetBtn = document.getElementById('resetFiltersBtn');
    const resetBtnInline = document.getElementById('resetFiltersBtnInline');
    const noResultsMessage = document.getElementById('noResultsMessage');

    const typeChips = Array.from(document.querySelectorAll('#typeChips .lesson-type-chip'));
    const viewAllLinks = Array.from(document.querySelectorAll('.view-all-link'));

    const state = {
        search: '',
        type: '',
        duration: ''
    };

    function normalize(value) {
        return (value ?? '').toString().trim().toLowerCase();
    }

    function matchesDuration(filterValue, hoursValue) {
        if (!filterValue) return true;

        const hours = Number(hoursValue ?? 0);
        if (Number.isNaN(hours)) return true;

        if (filterValue === 'short') return hours < 5;
        if (filterValue === 'medium') return hours >= 5 && hours <= 15;
        if (filterValue === 'long') return hours > 15;

        return true;
    }

    function setActiveChip(type) {
        typeChips.forEach(chip => {
            chip.classList.toggle('is-active', (chip.dataset.type ?? '') === (type ?? ''));
        });
    }

    function setType(type) {
        state.type = type ?? '';
        if (categoryFilter) categoryFilter.value = state.type;
        setActiveChip(state.type);
        applyFilters();
    }

    function applyFilters() {
        state.search = normalize(searchInput?.value);
        state.duration = durationFilter?.value ?? '';

        const cards = Array.from(document.querySelectorAll('[data-course-card]'));
        const sections = Array.from(document.querySelectorAll('[data-course-section]'));

        let visibleCards = 0;

        cards.forEach(card => {
            const title = normalize(card.dataset.title);
            const description = normalize(card.dataset.description);
            const type = card.dataset.type ?? '';
            const duration = card.dataset.duration;

            const matchesSearch = !state.search || title.includes(state.search) || description.includes(state.search);
            const matchesType = !state.type || type === state.type;
            const matchesDur = matchesDuration(state.duration, duration);

            const isVisible = matchesSearch && matchesType && matchesDur;
            card.style.display = isVisible ? '' : 'none';
            if (isVisible) visibleCards++;
        });

        sections.forEach(section => {
            const sectionCards = Array.from(section.querySelectorAll('[data-course-card]'));
            const anyVisible = sectionCards.some(card => card.style.display !== 'none');
            section.style.display = anyVisible ? '' : 'none';
        });

        if (noResultsMessage) {
            const show = cards.length > 0 && visibleCards === 0;
            noResultsMessage.classList.toggle('is-visible', show);
        }
    }

    function resetFilters() {
        if (searchInput) searchInput.value = '';
        if (categoryFilter) categoryFilter.value = '';
        if (durationFilter) durationFilter.value = '';
        setType('');
        applyFilters();
    }

    if (searchInput) {
        let timerId;
        searchInput.addEventListener('input', () => {
            clearTimeout(timerId);
            timerId = setTimeout(applyFilters, 200);
        });
    }

    if (categoryFilter) {
        categoryFilter.addEventListener('change', () => {
            setType(categoryFilter.value ?? '');
        });
    }

    if (durationFilter) {
        durationFilter.addEventListener('change', applyFilters);
    }

    typeChips.forEach(chip => {
        chip.addEventListener('click', () => setType(chip.dataset.type ?? ''));
    });

    viewAllLinks.forEach(link => {
        link.addEventListener('click', () => setType(link.dataset.type ?? ''));
    });

    if (resetBtn) resetBtn.addEventListener('click', resetFilters);
    if (resetBtnInline) resetBtnInline.addEventListener('click', resetFilters);

    applyFilters();
});
