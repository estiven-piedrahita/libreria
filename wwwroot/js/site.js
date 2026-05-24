document.addEventListener("DOMContentLoaded", () => {
  document.querySelectorAll("[data-table-search]").forEach((input) => {
    const table = document.querySelector(input.dataset.tableSearch);
    if (!table) return;

    const columns = table.querySelectorAll("thead th").length || 1;
    const emptyRow = document.createElement("tr");
    emptyRow.className = "no-results-row";
    emptyRow.hidden = true;
    emptyRow.innerHTML = `<td colspan="${columns}">No se encontraron resultados.</td>`;
    table.querySelector("tbody")?.appendChild(emptyRow);

    input.addEventListener("input", () => {
      const term = input.value.trim().toLowerCase();
      let visibleRows = 0;

      table.querySelectorAll("tbody tr:not(.no-results-row)").forEach((row) => {
        row.hidden = term.length > 0 && !row.innerText.toLowerCase().includes(term);
        if (!row.hidden) visibleRows += 1;
      });

      emptyRow.hidden = visibleRows > 0;
    });
  });

  document.querySelectorAll(".app-alert").forEach((alert) => {
    window.setTimeout(() => {
      alert.style.transition = "opacity .2s ease";
      alert.style.opacity = "0";
      window.setTimeout(() => alert.remove(), 220);
    }, 3500);
  });
});
