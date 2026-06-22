/* ══════════════════════════════════════════════
   lang.js — AR/EN Language Toggle
   Uses data-lang-ar / data-lang-en attributes
══════════════════════════════════════════════ */

(function () {
  'use strict';

  const STORAGE_KEY = 'cafeMazajLang';
  const DEFAULT_LANG = 'ar';

  function getLang() {
    return localStorage.getItem(STORAGE_KEY) || DEFAULT_LANG;
  }

  function applyLang(lang) {
    const html = document.getElementById('html-root') || document.documentElement;

    if (lang === 'ar') {
      html.setAttribute('lang', 'ar');
      html.setAttribute('dir', 'rtl');
    } else {
      html.setAttribute('lang', 'en');
      html.setAttribute('dir', 'ltr');
    }

    // Show/hide elements by language
    document.querySelectorAll('[data-lang-ar]').forEach(el => {
      el.style.display = lang === 'ar' ? '' : 'none';
    });
    document.querySelectorAll('[data-lang-en]').forEach(el => {
      el.style.display = lang === 'en' ? '' : 'none';
    });
  }

  window.toggleLang = function () {
    const current = getLang();
    const next = current === 'ar' ? 'en' : 'ar';
    localStorage.setItem(STORAGE_KEY, next);
    applyLang(next);
  };

  // Apply on load
  document.addEventListener('DOMContentLoaded', function () {
    applyLang(getLang());
  });
})();
