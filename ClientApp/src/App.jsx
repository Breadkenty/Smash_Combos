import React, { useState } from 'react'
import { Route, Switch } from 'react-router'

import './style/main.scss'

import { TopNav } from './components/TopNav'

import { SignUp } from './pages/SignUp'
import { LogIn } from './pages/LogIn'

import { Character } from './pages/Character'
import { Characters } from './pages/Characters'

import { CharacterCombo } from './pages/CharacterCombo'

import { SubmitCombo } from './pages/SubmitCombo'

import { SideNav } from './components/SideNav'
import { AddCharacter } from './pages/AddCharacter'
import { EditCharacter } from './pages/EditCharacter'
import { EditCombo } from './pages/EditCombo'

export function App() {
  const [sideNavDisplay, setSideNavDisplay] = useState(false)

  const handleSideBar = () => {
    if (sideNavDisplay) {
      setSideNavDisplay(false)
    } else {
      setSideNavDisplay(true)
    }
  }

  return (
    <>
      <SideNav handleSideBar={handleSideBar} sideNavDisplay={sideNavDisplay} />
      <div className={sideNavDisplay ? 'blur' : 'none'}>
        <TopNav handleSideBar={handleSideBar} />
        <Switch>
          <Route exact path="/signup">
            <SignUp />
          </Route>
          <Route exact path="/login">
            <LogIn />
          </Route>
          <Route exact path="/">
            <Characters />
          </Route>
          <Route exact path="/character/:characterVariableName">
            <Character />
          </Route>
          <Route exact path="/character/:characterVariableName/:comboId">
            <CharacterCombo />
          </Route>
          <Route exact path="/submit">
            <SubmitCombo />
          </Route>
          <Route exact path="/add">
            <AddCharacter />
          </Route>
          <Route exact path="/edit">
            <EditCharacter />
          </Route>
          <Route exact path="/character/:characterVariableName/:comboId/edit">
            <EditCombo />
          </Route>
        </Switch>
        <footer className="bottom-footer">
          <p>
            Made with love by <a href="#">Kento Kawakami</a>
          </p>
          <div>
            <a href="https://github.com/Breadkenty">
              <i className="fab fa-github"></i>
            </a>
            <a href="https://www.instagram.com/kento.kawakami/">
              <i className="fab fa-instagram"></i>
            </a>
          </div>
        </footer>
      </div>
    </>
  )
}
