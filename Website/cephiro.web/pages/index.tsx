import { Auth, ThemeSupa } from '@supabase/auth-ui-react'
import { useSession, useSupabaseClient } from '@supabase/auth-helpers-react'
import { Button } from '@/components/buttons/base.button'
import { ThemeProvider } from 'styled-components'
import { darktheme } from '@/components/common/theme'
import { GlobalStyle } from '@/components/common/global'
import { Main } from '@/components/common/main'
import { ButtonContent } from '@/components/buttons/content.button'
import { FiMail, FiSearch } from "react-icons/fi"
import { IconSize } from '@/components/common/icon'
import { InputBox, TextInput } from '@/components/input/base.text.input'
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
          <div style={{display: 'flex', flexDirection: 'column', gap: '0.7rem'}}>
            <label htmlFor="search">Rechercher un Article</label>
            <div style={{display: 'flex', flexDirection: 'row', gap: '0.7rem'}}>
              <InputBox>
                <FiMail size={IconSize.tiny} strokeWidth="2px"/>
                <TextInput id='search' autoComplete='false' withIcon={true} type='number' required/>
              </InputBox>
              <Button size="tiny" button="primary">
                <ButtonContent direction="row" gap='0.5rem' justifyContent='center'>
                  <FiSearch size={IconSize.tiny} strokeWidth="2px"/>
                  <p>Rechercher</p>
                </ButtonContent> 
              </Button>
              </div>
          </div>
        )}  
      </Main>
    </ThemeProvider>
  )
}

export default Home