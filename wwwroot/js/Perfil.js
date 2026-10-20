document.addEventListener("DOMContentLoaded", function () {
    const tagButtons = document.querySelectorAll(".perfil-etiqueta");
    const selectedTagsContainer = document.getElementById("selectedTagsContainer");

    if (!tagButtons.length || !selectedTagsContainer) {
        return;
    }

    function syncHiddenInputs() {
        const selectedIds = Array.from(tagButtons)
            .filter(button => button.classList.contains("active"))
            .map(button => button.getAttribute("data-id"));

        const currentInputs = Array.from(selectedTagsContainer.querySelectorAll("input[name='IdsEtiquetas']"));
        currentInputs.forEach(input => input.remove());

        selectedIds.forEach(id => {
            const hiddenInput = document.createElement("input");
            hiddenInput.type = "hidden";
            hiddenInput.name = "IdsEtiquetas";
            hiddenInput.value = id;
            selectedTagsContainer.appendChild(hiddenInput);
        });
    }

    tagButtons.forEach(button => {
        button.addEventListener("click", function () {
            this.classList.toggle("active");
            this.setAttribute("aria-pressed", this.classList.contains("active") ? "true" : "false");
            syncHiddenInputs();
        });
    });

    syncHiddenInputs();
});
