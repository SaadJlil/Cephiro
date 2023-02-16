import { Auth, ThemeSupa } from '@supabase/auth-ui-react'
import { useSession, useSupabaseClient } from '@supabase/auth-helpers-react'
import { Button } from '@/components/buttons/base.button'
import { ThemeProvider } from 'styled-components'
import { darktheme } from '@/components/common/theme'
import { GlobalStyle } from '@/components/common/global'
import { Main } from '@/components/common/main'
import { ButtonContent } from '@/components/buttons/content.button'

const Home = () => {
  const session = useSession()
  const supabase = useSupabaseClient()

  return (
    <ThemeProvider theme={darktheme}>
      <GlobalStyle />
      <Main>
        {!session ? (
          <Auth 
            providers={["google", "github"]}
            supabaseClient={supabase} 
            appearance={{ theme: ThemeSupa }} 
            theme="dark" />
        ) : (
          <Button size="tiny" button="text" block>
            <ButtonContent>
              <p>Ajouter un Produit</p>
              <p>Ajouter un Produit</p>
            </ButtonContent>
          </Button>
        )}  
      </Main>
    </ThemeProvider>
  )
}

export default Home