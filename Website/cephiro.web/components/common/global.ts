import { createGlobalStyle } from 'styled-components'

export const GlobalStyle = createGlobalStyle<{theme: any}>`
  * {
        box-sizing: border-box;
        padding: 0;
        margin: 0;
    }

    html, body {
        max-width: 100vw;
        overflow-x: hidden;
        height: 100%;
    }

  body {
    background-color: ${props => props.theme.default1};
  }

  #__next {
    min-height: 100vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
  }

  a {
    color: inherit;
    text-decoration: none;
  }
`