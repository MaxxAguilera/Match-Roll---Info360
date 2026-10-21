document.addEventListener("DOMContentLoaded", function () {
    const selectedTagsContainer = document.getElementById("selectedTagsContainer");
    const tagButtons = document.querySelectorAll(".perfil-etiqueta");

    function syncHiddenInputs() {
        if (!selectedTagsContainer) return;

        const currentInputs = Array.from(selectedTagsContainer.querySelectorAll("input[name='IdsEtiquetas']"));
        currentInputs.forEach(input => input.remove());

        const selectedIds = Array.from(document.querySelectorAll(".perfil-etiqueta.active"))
            .map(button => button.getAttribute("data-id"))
            .filter(Boolean);

        selectedIds.forEach(id => {
            const hiddenInput = document.createElement("input");
            hiddenInput.type = "hidden";
            hiddenInput.name = "IdsEtiquetas";
            hiddenInput.value = id;
            selectedTagsContainer.appendChild(hiddenInput);
        });
    }

    function toggleEtiqueta(button) {
        if (!button || !button.classList.contains("perfil-etiqueta")) return;

        const isActive = button.classList.contains("active");
        button.classList.toggle("active", !isActive);
        button.setAttribute("aria-pressed", String(!isActive));
        syncHiddenInputs();
    }

    if (tagButtons.length) {
        tagButtons.forEach(button => {
            button.addEventListener("click", function (event) {
                event.preventDefault();
                event.stopPropagation();
                toggleEtiqueta(this);
            });
        });
    }

    document.addEventListener("click", function (event) {
        const button = event.target.closest(".perfil-etiqueta");
        if (!button) return;

        event.preventDefault();
        event.stopPropagation();
        toggleEtiqueta(button);
    });

    syncHiddenInputs();
});

function normalizarVideoUrl(url) {
    const value = (url || "").trim();
    if (!value) return "";

    const youtubeMatch = value.match(/(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:watch\?v=|embed\/)|youtu\.be\/|youtube-nocookie\.com\/embed\/)([A-Za-z0-9_-]{11})/i);
    if (youtubeMatch) {
        const videoId = youtubeMatch[1];
        return `https://www.youtube-nocookie.com/embed/${videoId}?autoplay=1&mute=1&rel=0&modestbranding=1&showinfo=0&controls=1&playsinline=1&enablejsapi=1`;
    }

    return value;
}
