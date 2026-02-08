 <script>
  // ===== existing mobile filter toggle =====
  const btn = document.querySelector(".filter-toggle");
  const filters = document.querySelector(".filters");
  btn?.addEventListener("click", () => {
    filters.classList.toggle("filters--open");
  });

  // ===== Seat modal logic =====
  const seatModal = document.getElementById("seatModal");
  const seatGrid = document.getElementById("seatGrid");
  const selectedCountEl = document.getElementById("selectedCount");
  const maxSelectableEl = document.getElementById("maxSelectable");
  const selectedSeatsEl = document.getElementById("selectedSeats");
  const continueBtn = document.getElementById("continueBtn");

  // Mock occupied seats (change anytime)
  const occupiedSeats = new Set(["3A","4B","4C","7B","9C","10A"]);
  let selectedSeats = new Set();
  let maxSelectable = 2;

  function openModal(){
    seatModal.classList.add("is-open");
    seatModal.setAttribute("aria-hidden", "false");
    document.body.style.overflow = "hidden";
  }

  function closeModal(){
    seatModal.classList.remove("is-open");
    seatModal.setAttribute("aria-hidden", "true");
    document.body.style.overflow = "";
  }

  // Build a simple 12-row x 3-column seat map (A,B,C)
  function buildSeats(rows = 12){
    seatGrid.innerHTML = "";
    selectedSeats = new Set();
    updateSummary();

    const cols = ["A","B","C"];
    for(let r=1; r<=rows; r++){
      for(const c of cols){
        const seatId = `${r}${c}`;
        const button = document.createElement("button");
        button.type = "button";
        button.className = "seat";
        button.textContent = seatId;
        button.dataset.seat = seatId;

        if(occupiedSeats.has(seatId)){
          button.classList.add("is-occupied");
          button.disabled = true;
        }

        button.addEventListener("click", () => toggleSeat(seatId, button));
        seatGrid.appendChild(button);
      }
    }
  }

  function toggleSeat(seatId, el){
    if (occupiedSeats.has(seatId)) return;

    if (selectedSeats.has(seatId)){
      selectedSeats.delete(seatId);
      el.classList.remove("is-selected");
    } else {
      if (selectedSeats.size >= maxSelectable){
        // simple feedback
        alert(`You can select up to ${maxSelectable} seat(s).`);
        return;
      }
      selectedSeats.add(seatId);
      el.classList.add("is-selected");
    }
    updateSummary();
  }

  function updateSummary(){
    selectedCountEl.textContent = String(selectedSeats.size);
    maxSelectableEl.textContent = String(maxSelectable);
    selectedSeatsEl.textContent = selectedSeats.size ? Array.from(selectedSeats).join(", ") : "-";
    continueBtn.disabled = selectedSeats.size !== maxSelectable; // require exact pax
  }

  // Close when clicking backdrop or X
  seatModal.addEventListener("click", (e) => {
    if (e.target?.dataset?.close === "true") closeModal();
  });
  document.addEventListener("keydown", (e) => {
    if (e.key === "Escape" && seatModal.classList.contains("is-open")) closeModal();
  });

  // Hook: open modal when user clicks any Select button
  document.querySelectorAll(".btn--select").forEach((button) => {
    button.addEventListener("click", () => {
      // Pax comes from top mini-search
      const paxSelect = document.getElementById("pax");
      maxSelectable = parseInt(paxSelect?.value || "1", 10);

      buildSeats(18); // show more rows to allow scrolling
      openModal();
    });
  });

  // Continue button (for now, just closes modal)
  continueBtn.addEventListener("click", () => {
    alert(`Selected seats: ${Array.from(selectedSeats).join(", ")}`);
    closeModal();
    // Later: redirect to passenger-details page, e.g. window.location.href="checkout.html";
  });

  // Initial seat grid build (optional; modal rebuilds on open anyway)
  buildSeats(18);
</script>