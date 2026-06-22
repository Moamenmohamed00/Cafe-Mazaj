/* ══════════════════════════════════════════════
   admin.js
══════════════════════════════════════════════ */

function toggleSidebar() {
  const sidebar = document.getElementById('admin-sidebar');
  if (sidebar) sidebar.classList.toggle('open');
}

// Image preview on file input change
document.addEventListener('DOMContentLoaded', function () {
  const imgInput = document.getElementById('imageFileInput');
  const imgPreview = document.getElementById('imagePreview');
  if (imgInput && imgPreview) {
    imgInput.addEventListener('change', function () {
      const file = this.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onload = e => { imgPreview.src = e.target.result; imgPreview.style.display = 'block'; };
        reader.readAsDataURL(file);
      }
    });
  }

  // Confirm delete modals
  document.querySelectorAll('[data-confirm]').forEach(btn => {
    btn.addEventListener('click', function (e) {
      if (!confirm(this.getAttribute('data-confirm'))) e.preventDefault();
    });
  });
});
